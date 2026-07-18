using FactoryGuardian.Repositories;
using System.Windows.Controls;

namespace FactoryGuardian.views;

public partial class EquipmentListView : UserControl
{

    public EquipmentListView()
    {
        InitializeComponent();

        DataContext =
            new EquipmentViewModel();
    }

}