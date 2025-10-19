
using System.Security.Cryptography.X509Certificates;
using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Services;
using Manta.Infrastructure.Repositories;
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
    private FShipments _shipmentsForm;
    private FShipments _shipmentsReadyForPickUpForm;
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
        
        _shipmentsForm = new FShipments(_searchService);
        _optionsForm = new FOptions(userRepository, deliveryPointRepository);
        _shipmentsReadyForPickUpForm = new FShipments(_searchService, parcel => parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup);
        _cashDeskForm = new FCashDesk(deliveryService);
        
        _sideMenuButtons = [shipmentDeliveryBtn, shipmentsBtn, returnRequestBtn, adminToolsBtn, optionsBtn];
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

    private void ChangeForm(Form form, object sender)
    {
        var clickedBtn = (Button)sender;
        foreach (var btn in _sideMenuButtons)
        {
            btn.BackColor = System.Drawing.Color.FromArgb(((int)((byte)59)), ((int)((byte)77)), ((int)((byte)86)));
        }
        clickedBtn.BackColor = System.Drawing.Color.FromArgb(((int)((byte)54)), ((int)((byte)71)), ((int)((byte)79)));
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
        ChangeForm(_shipmentsReadyForPickUpForm, sender);
    }

    private void shipmentsBtn_Click(object sender, EventArgs e)
    {
        ChangeForm(_shipmentsForm, sender);
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
}