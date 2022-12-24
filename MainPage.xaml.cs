using Scratch.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Android.Content.ClipData;

namespace Scratch
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<NoteItem> Notes { get; set; } = new();
        //int count = 0;
        //public ICommand SelectNote => new Command(() => ShowNoteInEditor());

        public MainPage()
        {
            InitializeComponent();
            //NotesCollectionView.ItemsSource = Notes;

            BindingContext = this;
            Init();
        }

        private void Init()
        {
            var note = new NoteItem();
            note.Text = "Take out the trash!";
            note.CreatedDate = DateTime.Now;
            Notes.Add(note);

            var note2 = new NoteItem();
            note2.Text = "Take out the sh!";
            note2.CreatedDate = DateTime.Now;
            Notes.Add(note2);

            var note3 = new NoteItem();
            note3.Text = "Take out th!";
            note3.CreatedDate = DateTime.Now;
            Notes.Add(note3);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("hi", $"You invoked the next action.", "OK");
            //count++;
            //CounterLabel.Text = $"Current count: {count}";

            //SemanticScreenReader.Announce(CounterLabel.Text);
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            await App.Current.MainPage.DisplayAlert(item.Text, $"You invoked the {item.Text} action.", "OK");
        }

        private async void SelectNote(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("hi", $"You invoked the next action.", "OK");
        }
    }
}