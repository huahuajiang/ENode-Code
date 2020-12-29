using ENode.Domain;

namespace NoteSample.Domain
{
    public class Note:AggregateRoot<string>
    {
        private string _title;

        public string Title { get => _title; }


        public Note(string id,string title) : base(id)
        {
            ApplyEvent(new NoteCreated(title));
        }

        public void ChangeTitle(string title)
        {
            ApplyEvent(new NoteTitleChanged(title));
        }

        
    }
}
