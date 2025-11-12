using LoginUserAdmin.Models;
using LoginUserAdmin.Services;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoginUserAdmin
{
    public partial class MainPage : ContentPage
    {
        private readonly LoginAdminService _loginService;

        public MainPage()
        {
            InitializeComponent();

            var settings = new SettingsService();
            _loginService = new LoginAdminService(settings);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PwdEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Aviso", "Preencha email e senha.", "OK");
                return;
            }

            try
            {
                LoginButton.IsEnabled = false;
                var originalText = LoginButton.Text;
                LoginButton.Text = "Entrando...";

                var response = await _loginService.LoginAsync(email, password);

                if (response.IsSuccessStatusCode)
                {
                    // 🔹 Lê o JSON retornado
                    var json = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    // 🔹 Verifica se o campo user.isAdmin existe e é true
                    if (root.TryGetProperty("user", out var userElem) &&
                        userElem.TryGetProperty("isAdmin", out var isAdminElem) &&
                        isAdminElem.GetBoolean())
                    {
                        // ✅ É admin → entra no Dashboard
                        await Navigation.PushModalAsync(new DashboardPage());
                    }
                    else
                    {
                        // ❌ Não é admin → bloqueia
                        await DisplayAlert("Acesso negado", "Apenas administradores podem acessar o painel.", "OK");
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Erro", $"Falha no login: {error}", "OK");
                }

                LoginButton.Text = originalText;
                LoginButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                LoginButton.Text = "Login";
                LoginButton.IsEnabled = true;
                await DisplayAlert("Erro", $"Exceção: {ex.Message}", "OK");
            }
        }
    }
}
