namespace SharpMud.Commands
{
    public class North : Move,ICommand
    {
        private readonly string[] _accessWords = { "NORTH", "N" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "NORTH"; }
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
