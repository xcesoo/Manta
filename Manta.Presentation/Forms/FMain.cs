
using System.Security.Cryptography.X509Certificates;
using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Services;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Controls;
using Manta.Presentation.Services;

namespace Manta.Presentation.Forms;

public partial class FMain : Form
{
    //repositories
    private IParcelRepository _parcelRepository;
    private IDeliveryPointRepository _deliveryPointRepository;
    private IDeliveryVehicleRepository _deliveryVehicleRepository;
    private IUserRepository _userRepository;
    
    //services
    private ParcelDeliveryService _deliveryService;
    private ParcelStatusService _statusService;
    private ParcelSearchService _searchService;
    
    //forms
    private FCashDesk _cashDeskForm;
    private FOptions _optionsForm;
    private Button[] _sideMenuButtons;
    public  FMain(IParcelRepository parcelRepository, 
        IDeliveryPointRepository deliveryPointRepository, 
        IDeliveryVehicleRepository deliveryVehicleRepository, 
        IUserRepository userRepository, 
        ParcelDeliveryService deliveryService, 
        ParcelStatusService statusService)
    {
        InitializeComponent();
        
        _parcelRepository = parcelRepository;
        _deliveryPointRepository = deliveryPointRepository;
        _deliveryVehicleRepository = deliveryVehicleRepository;
        _userRepository = userRepository;
        
        _deliveryService = deliveryService;
        _statusService = statusService;
        _searchService = new ParcelSearchService(parcelRepository);
        
        _cashDeskForm = new FCashDesk(deliveryService);
        _optionsForm = new FOptions(userRepository, deliveryPointRepository);
        
        _sideMenuButtons = [shipmentDeliveryBtn, shipmentsBtn, returnRequestBtn, adminToolsBtn, optionsBtn, acceptParcels];
        _cashDeskForm.TopLevel = false;
        _cashDeskForm.Dock = DockStyle.Fill;
        cashDeskPanel.Controls.Add(_cashDeskForm);
        cashDeskPanel.Tag = _cashDeskForm;
        _cashDeskForm.BringToFront();
        _cashDeskForm.Show();
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

    private void ChangeForm(Form form, object? sender)
    {
        if (sender != null)
        {
            var clickedBtn = (Button)sender;
            foreach (var btn in _sideMenuButtons)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(((int)((byte)59)), ((int)((byte)77)), ((int)((byte)86)));
            }

            clickedBtn.BackColor =
                System.Drawing.Color.FromArgb(((int)((byte)54)), ((int)((byte)71)), ((int)((byte)79)));
        }
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
        var shipmentsReadyForPickUpForm = new FShipments(_searchService,
            parcel => parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup);
        shipmentsReadyForPickUpForm.ShipmentOpenRequested += parcel =>
        {
            ChangeForm(new FShipmentInfo(parcel, _deliveryService, _parcelRepository), null);
        };
        ChangeForm(shipmentsReadyForPickUpForm, sender);
    }

    private void shipmentsBtn_Click(object sender, EventArgs e)
    {
        var shipmentsForm = new FShipments(_searchService);
        shipmentsForm.ShipmentOpenRequested += parcel =>
        {
            ChangeForm(new FShipmentInfo(parcel, _deliveryService, _parcelRepository), null);
        };
        ChangeForm(shipmentsForm, sender);
    }

    private void optionsBtn_Click(object sender, EventArgs e)
    {
        ChangeForm(_optionsForm, sender);
    }

    private void exitBtn_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.Application.Exit();
    }

    private void minimizeBtn_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized; 
    }

    private void acceptParcels_Click(object sender, EventArgs e)
    {
        ChangeForm(new FAcceptShipments(_deliveryService, _parcelRepository), sender);
    }

    private void returnRequestBtn_Click(object sender, EventArgs e)
    {
        var toReturnForm = new FToReturn(_parcelRepository, _deliveryService);
        toReturnForm.ShipmentOpenRequested += parcel => ChangeForm(new FShipmentInfo(parcel, _deliveryService, _parcelRepository), null);
        ChangeForm(toReturnForm, sender);
    }
}