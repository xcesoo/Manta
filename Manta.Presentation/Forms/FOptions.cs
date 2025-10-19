using Manta.Infrastructure.Repositories;
using Manta.Presentation.State;
using Microsoft.IdentityModel.Tokens;
using AppContext = Manta.Presentation.State.AppContext;

namespace Manta.Presentation.Forms;

public partial class FOptions : Form
{
    private IUserRepository _userRepository;
    private IDeliveryPointRepository _deliveryPointRepository;
    public FOptions(IUserRepository userRepository, IDeliveryPointRepository deliveryPointRepository)
    {
        _userRepository = userRepository;
        _deliveryPointRepository = deliveryPointRepository;
        InitializeComponent();
    }

    private async void userTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (userTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Некоректні дані", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;           
            }
            AppContext.CurrentUser = await _userRepository.GetByEmailAsync(userTextBox.Text) ?? null;
            if (AppContext.CurrentUser == null) MessageBox.Show("Користувача не знайдено", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void deliveryPointIdTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (deliveryPointIdTextBox.Text.IsNullOrEmpty() || deliveryPointIdTextBox.Text.Any(c => !char.IsDigit(c)))
            {
                MessageBox.Show("Некоректні дані", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AppContext.CurrentDeliveryPointId =
                await _deliveryPointRepository.ExistsAsync(Convert.ToInt32(deliveryPointIdTextBox.Text))
                    ? Convert.ToInt32(deliveryPointIdTextBox.Text) : null;
            if (AppContext.CurrentDeliveryPointId == null) MessageBox.Show("Точку видачі не знайдено", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AppContext.DeliveryPointChanged();
        }
    }
}