
using System.Security.Cryptography.X509Certificates;
using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Services;
using Manta.Infrastructure.Repositories;

namespace Manta.Presentation.Forms;

public partial class FMain : Form
{
    private IParcelRepository _parcelRepository;
    private IDeliveryPointRepository _deliveryPointRepository;
    private IDeliveryVehicleRepository _deliveryVehicleRepository;
    private IUserRepository _userRepository;
    private ParcelDeliveryService _deliveryService;
    private ParcelStatusService _statusService;
    private User _user;
    private int _currentDeliveryPointId;
    private FShipments _shipmentsForm;
    public  FMain(IParcelRepository parcelRepository, 
        IDeliveryPointRepository deliveryPointRepository, 
        IDeliveryVehicleRepository deliveryVehicleRepository, 
        IUserRepository userRepository, 
        ParcelDeliveryService deliveryService, 
        ParcelStatusService statusService)
    {
        _parcelRepository = parcelRepository;
        _deliveryPointRepository = deliveryPointRepository;
        _deliveryVehicleRepository = deliveryVehicleRepository;
        _userRepository = userRepository;
        _deliveryService = deliveryService;
        _statusService = statusService;
        _shipmentsForm = new FShipments(parcelRepository);
        InitializeComponent();
    }
    private bool FormDragged = false;
    private Point StartMousePosition;

    private void borderPanel_MouseDown(object sender, MouseEventArgs e)
    {
        FormDragged = true;
        StartMousePosition = e.Location;
    }

    private void borderPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (FormDragged)
        {
            Point screenPoint = PointToScreen(e.Location);
            Location = new Point(screenPoint.X - StartMousePosition.X, screenPoint.Y - StartMousePosition.Y);
        }
    }

    private void borderPanel_MouseUp(object sender, MouseEventArgs e) => FormDragged = false;

    private Form _activeForm;

    private void ChangeForm(Form form)
    {
        if (form == _activeForm) return;
        if (_activeForm != null) _activeForm.Hide();
        _activeForm = form;
        form.TopLevel = false;
        form.Dock = DockStyle.Fill;
        mainPanel.Controls.Add(form);
        mainPanel.Tag = form;
        form.BringToFront();
        form.Show();
    }

    private void shipmentDelivery_Click(object sender, EventArgs e)
    {
        
    }

    private void shipmentsBtn_Click(object sender, EventArgs e) => ChangeForm(_shipmentsForm);
}