using ActorReposLib.Interfaces;

namespace ActorReposLib
{
    public class Actor : IIdentifiable
    {
        private string _name;
        private int _id;
        private int _birthyear;
       

        public int Id { 
            get => _id;
            set {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("id must be higher than 0");
                _id = value; }

        }
        public string? Name { 
            get => _name;
            set {
                if (value == null)

                    throw new ArgumentNullException("Name is null");

                if (value.Length < 4)

                    throw new ArgumentOutOfRangeException("Name must be more than 4 letters");

                
                _name = value; }
        }
        public int BirthYear
        {
            get => _birthyear;
            set { 
                
                if (value < 1820)
                    throw new ArgumentOutOfRangeException("Birthyear must be higher than 1820");
                _birthyear = value; 
            }
        }
        public Actor()
        {
            
        }
        public override string ToString()
        {
            return $"Name of actor: {Name} Birthyear of actor: {BirthYear}";
        }
    }
}
