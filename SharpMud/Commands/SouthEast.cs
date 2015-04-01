namespace SharpMud.Commands
{
    public class SouthEast : Move,ICommand
    {
        private readonly string[] _accessWords = { "SOUTHEAST", "SE" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "SOUTHEAST"; }
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
