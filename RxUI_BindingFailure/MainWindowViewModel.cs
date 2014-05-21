using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RxUI_BindingFailure
{
    public class MainWindowViewModel : ReactiveObject
    {

        public MainWindowViewModel()
        {
            Items = new ReactiveList<string>(new List<string> { "Item1", "Item2", "Item3" });

            var canGoTo = this.WhenAny(vm => vm.SelectedItem, x => !string.IsNullOrEmpty(x.Value));
            GoToCmd = ReactiveCommand.Create(canGoTo);
            
            GoToCmd.Subscribe(_ =>
            {
                Console.WriteLine("I made it here, so the SelectedItem binding must be fixed.");
            });
        }

        public ReactiveCommand<Object> GoToCmd { get; protected set; }
        public ReactiveList<string> Items { get; protected set; }

        private string _SelectedItem;
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set { this.RaiseAndSetIfChanged(ref _SelectedItem, value); }
        }
    }
}
