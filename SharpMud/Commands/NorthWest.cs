namespace SharpMud.Commands
{
    public class NorthWest : Move,ICommand
    {
        private readonly string[] _accessWords = { "NORTHWEST", "NW" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "NORTHWEST"; }
        }


        public Permission[] PermissionsRequired
        {
            get
            {
                Permission[] p = { World.Db.Permissions.GetByName("none"), World.Db.Permissions.GetByName("all") };
                return p;
            }
        }
    }
}
