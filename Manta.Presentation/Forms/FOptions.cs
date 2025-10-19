using Manta.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;

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
            Globals.CurrentUser = await _userRepository.GetByEmailAsync(userTextBox.Text) ?? null;
            if (Globals.CurrentUser == null) MessageBox.Show("Користувача не знайдено", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Globals.CurrentDeliveryPointId =
                await _deliveryPointRepository.ExistsAsync(Convert.ToInt32(deliveryPointIdTextBox.Text))
                    ? Convert.ToInt32(deliveryPointIdTextBox.Text) : null;
            if (Globals.CurrentDeliveryPointId == null) MessageBox.Show("Точку видачі не знайдено", "MantaException", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}