namespace SharpMud.Commands
{
    public class NorthEast : Move,ICommand
    {
        private readonly string[] _accessWords = { "NORTHEAST", "NE" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "NORTHEAST"; }
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
