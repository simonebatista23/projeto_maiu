using Microcharts;
using SkiaSharp;
using LoginUserAdmin.Models;
using LoginUserAdmin.Services;
using System.Globalization;

namespace LoginUserAdmin
{
    public partial class DashboardPage : ContentPage
    {
        private readonly TicketService _ticketService;
        private List<TicketDto> _tickets = new();
        private bool _isRunning = true;
        private int _ultimoTotal = -1; // guarda o total anterior para comparar

        public DashboardPage()
        {
            InitializeComponent();
            _ticketService = new TicketService();

            LoadMonths();
            StartAutoRefresh();
        }

        private void LoadMonths()
        {
            var meses = DateTimeFormatInfo.CurrentInfo.MonthNames
                .Where(m => !string.IsNullOrEmpty(m))
                .Select((m, i) => new { Nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(m), Numero = i + 1 })
                .ToList();

            foreach (var mes in meses)
                MonthPicker.Items.Add(mes.Nome);

            MonthPicker.SelectedIndex = DateTime.Now.Month - 1;
        }

        private async void StartAutoRefresh()
        {
            await Task.Run(async () =>
            {
                while (_isRunning)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        var mes = MonthPicker.SelectedIndex + 1;
                        await LoadDataIfChanged(mes);
                    });

                    await Task.Delay(1000);
                }
            });
        }

        private async Task LoadDataIfChanged(int mes)
        {
            try
            {
                var tickets = await _ticketService.GetTicketsAsync();
                var lista = tickets ?? new();

                var ticketsMes = lista
                    .Where(t => t.Open_datetime.Month == mes)
                    .ToList();

                // compara com o total anterior
                var totalAtual = ticketsMes.Count;
                if (totalAtual == _ultimoTotal)
                    return; // não muda nada na tela se não houve novos chamados

                _ultimoTotal = totalAtual;
                _tickets = lista;
                AtualizarGrafico(ticketsMes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
            }
        }

        private void AtualizarGrafico(List<TicketDto> ticketsMes)
        {
            TotalTicketsLabel.Text = ticketsMes.Count.ToString();

            var dadosPorDia = ticketsMes
                .GroupBy(t => t.Open_datetime.Day)
                .Select(g => new
                {
                    Dia = g.Key,
                    Quantidade = g.Count()
                })
                .OrderBy(g => g.Dia)
                .ToList();

            if (!dadosPorDia.Any())
            {
                TicketsChart.Chart = null;
                return;
            }

            var entries = dadosPorDia.Select(d => new ChartEntry(d.Quantidade)
            {
                Label = d.Dia.ToString(),
                ValueLabel = d.Quantidade.ToString(),
                Color = SKColor.Parse("#2563eb"),
                ValueLabelColor = SKColor.Parse("#6b7280"),
                TextColor = SKColor.Parse("#6b7280")
            }).ToList();

            var barChart = new BarChart
            {
                Entries = entries,
                LabelTextSize = 28,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                BackgroundColor = SKColor.Parse("#000000")
            };

            TicketsChart.Chart = barChart;
        }

        private void OnMonthChanged(object sender, EventArgs e)
        {
            if (MonthPicker.SelectedIndex >= 0)
            {
                var mes = MonthPicker.SelectedIndex + 1;
                _ultimoTotal = -1; // força atualização quando muda de mês
                _ = LoadDataIfChanged(mes);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _isRunning = false;
        }
    }
}
