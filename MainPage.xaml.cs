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
            //var note = new NoteItem();
            //note.Text = "Take out the trash!";
            //note.CreatedDate = DateTime.Now;
            //Notes.Add(note);

            //var note2 = new NoteItem();
            //note2.Text = "Take out the sh!";
            //note2.CreatedDate = DateTime.Now;
            //Notes.Add(note2);

            //var note3 = new NoteItem();
            //note3.Text = "Take out th!";
            //note3.CreatedDate = DateTime.Now;
            //Notes.Add(note3);
            //await _database.DeleteAllNotes();

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
                //Console.WriteLine(inserted.ToString());
                //Console.WriteLine(inserted.ToString());
                //Console.WriteLine(inserted.ToString());
            }
        }

        private async void SwipeItem_Invoked(NoteItem noteitem)
        {
            //var noteitem = sender as SwipeItem;
            //var found = await _database.GetNote(Convert.ToInt32(noteitem.Text));
            var deleted = await _database.DeleteNote(noteitem);
            //Console.WriteLine("555555555555555555555555555555");
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
                Console.WriteLine(removed);
                Console.WriteLine(noteitem.Id.ToString());
                Console.WriteLine(noteitem.Text);
            }
        }

        private async void ShowNoteInEditor(NoteItem noteitem)
        {
            //await App.Current.MainPage.DisplayAlert("hi", $"You invoked the next action.", "OK");
            Console.WriteLine(noteitem.ToString());
            //var noteitem = sender as ;
            var found = await _database.GetNote(noteitem.Id);
            Console.WriteLine("55555555555555");
            Console.WriteLine(found.Id.ToString());
            NoteEditor.Text = found.Text;
        }
    }
}