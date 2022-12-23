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

        private void OnCounterClicked(object sender, EventArgs e)
        {
            //count++;
            //CounterLabel.Text = $"Current count: {count}";

            //SemanticScreenReader.Announce(CounterLabel.Text);
        }
    }
}