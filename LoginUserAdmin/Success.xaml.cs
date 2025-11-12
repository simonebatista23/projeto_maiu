namespace LoginUserAdmin;

public partial class Success : ContentPage
{
	public Success()
	{
		InitializeComponent();
	}
    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(); 
    }
}