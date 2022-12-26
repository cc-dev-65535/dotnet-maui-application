using Microsoft.Maui;
using Scratch.Data;
using Scratch.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
//using static Android.Provider.ContactsContract.CommonDataKinds;
//using static Android.Content.ClipData;

namespace Scratch
{
    public partial class MainPage : ContentPage
    {
        // Binds to collection of notes in xaml view
        public ObservableCollection<NoteItem> Notes { get; set; } = new();
        // Binds to text in editor in xaml view
        public string EditorText { get; set; } = String.Empty;
        public ICommand SelectNote { get; private set; }
        public ICommand SwipeNote { get; private set; }
        readonly Database _database;

        public MainPage()
        {
            InitializeComponent();
            //NotesCollectionView.ItemsSource = Notes;

            BindingContext = this;

            SelectNote = new Command<NoteItem>((note) => ShowNoteInEditor(note));
            SwipeNote = new Command<NoteItem>((note) => SwipeItem_Invoked(note));

            _database = new Database();
            _ = Initialize();
        }

        private async Task Initialize()
        {
            //await _database.DeleteAllNotes();

            var dbnotes = await _database.GetNotes();
            foreach (var dbnote in dbnotes)
            {
                Notes.Add(dbnote);
                Console.WriteLine(dbnote.Id.ToString());
                Console.WriteLine(dbnote.Text.ToString());
                Console.WriteLine(dbnote.CreatedDate.ToString());
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var newnote = new NoteItem
            {
                Text = EditorText,
                CreatedDate = DateTime.Now
            };

            var inserted = await _database.AddNote(newnote);
            if (inserted != 0)
            {
                Notes.Add(newnote);
                NoteEditor.Text = String.Empty;
            }
        }

        private async void SwipeItem_Invoked(NoteItem noteitem)
        {
            var deleted = await _database.DeleteNote(noteitem);
            if (deleted != 0)
            {
                bool removed = false;
                foreach (var item in Notes.ToList())
                {
                    if (item.Id == noteitem.Id)
                    {
                        removed = Notes.Remove(item);
                    }
                }
            }
        }

        private async void ShowNoteInEditor(NoteItem noteitem)
        {
            var found = await _database.GetNote(noteitem.Id);
            NoteEditor.Text = found.Text;
        }
    }
}