using FactoryGuardian.Repositories;
using FactoryGuardian.ViewModels;
using System.Windows.Controls;

namespace FactoryGuardian.Views;

public partial class EquipmentListView : UserControl
{

    public EquipmentListView()
    {
        InitializeComponent();

        DataContext =
            new EquipmentViewModel();
    }

}