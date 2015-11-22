using System;
using CodeHub.iOS.Views;
using CodeHub.Core.ViewModels.Source;
using ReactiveUI;
using CodeHub.iOS.Cells;
using System.Reactive.Linq;
using UIKit;

namespace CodeHub.iOS.ViewControllers.Source
{
    public class CommitBranchesViewController : BaseTableViewController<CommitBranchesViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            EmptyView = new Lazy<UIView>(() =>
                new EmptyListView(Octicon.GitBranch.ToEmptyListImage(), "There are no branches."));

            TableView.RegisterClassForCellReuse(typeof(BranchCellView), BranchCellView.Key);
            var source = new ReactiveTableViewSource<BranchItemViewModel>(TableView, ViewModel.Items, BranchCellView.Key, 44f);
            this.WhenActivated(d => d(source.ElementSelected.OfType<BranchItemViewModel>().Subscribe(x => x.GoToCommand.ExecuteIfCan())));
            TableView.Source = source;
        }
    }
}
