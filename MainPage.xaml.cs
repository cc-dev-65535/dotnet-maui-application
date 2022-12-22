using Scratch.Models;
using System.Collections.ObjectModel;

namespace Scratch
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<NoteItem> Notes { get; set; } = new();
        //int count = 0;

        public MainPage()
        {
            InitializeComponent();
            NotesCollectionView.ItemsSource = Notes;

            Init();
        }

        private void Init()
        {
            var note = new NoteItem();
            note.Text = "Hi";
            note.CreatedDate = DateTime.Now;
            Notes.Add(note);
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            //count++;
            //CounterLabel.Text = $"Current count: {count}";

            //SemanticScreenReader.Announce(CounterLabel.Text);
        }
    }
}