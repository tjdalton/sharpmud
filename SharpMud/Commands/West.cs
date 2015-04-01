namespace SharpMud.Commands
{
    public class West : Move, ICommand
    {
        private readonly string[] _accessWords = { "WEST", "W" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }

        public string Help
        {
            get { return "WEST"; }
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
