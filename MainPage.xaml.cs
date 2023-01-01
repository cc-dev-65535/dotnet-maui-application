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
        public ICommand SelectNote => new Command<NoteItem>(async (note) => await ShowNoteInEditor(note));
        public ICommand SwipeNote => new Command<NoteItem>(async (note) => await SwipeItemInvoked(note));
        readonly Database _database;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            //SelectNote = new Command<NoteItem>((note) => ShowNoteInEditor(note));
            //SwipeNote = new Command<NoteItem>((note) => SwipeItem_Invoked(note));

            _database = new Database();
            //_ = Initialize();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Uncomment to refresh database table, or you can just wipe user data using android studio
            //await _database.DeleteAllNotes();

            var dbnotes = await _database.GetNotes();
            foreach (var dbnote in dbnotes)
            {
                Notes.Add(dbnote);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Notes.Clear();
            //Console.WriteLine(Notes.Count);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = false;
            
            if (EditorText == "")
            {
                return;
            }

            var newnote = new NoteItem
            {
                Text = EditorText,
                CreatedDate = DateTime.Now
            };

            var inserted = await _database.AddNote(newnote);
            if (inserted != 0)
            {
                //Notes.ShiftAll<NoteItem>();
                //Notes.Insert(0, newnote);
                Notes.Clear();
                var dbnotes = await _database.GetNotes();
                foreach (var dbnote in dbnotes)
                {
                    Notes.Add(dbnote);
                }
                NoteEditor.Text = String.Empty;

                await DisplayAlert("Saved", "Your note has been saved", "OK");
            }
            SaveButton.IsEnabled = true;
        }

        private async Task SwipeItemInvoked(NoteItem noteitem)
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
                        if (removed)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private async Task ShowNoteInEditor(NoteItem noteitem)
        {
            var found = await _database.GetNote(noteitem.Id);
            NoteEditor.Text = found.Text;
        }
    }

    public static class CollectionExtensions
    {
        public static void ShiftAll<T>(this ObservableCollection<T> collection)
        {
            int collectioncount = collection.Count;

            for (int i = 0; i < collectioncount; i++)
            {
                collection.Move(i, i + 1);
            }
        }
    }
}