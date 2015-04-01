namespace SharpMud.Commands
{
    public class SouthWest : Move,ICommand
    {
        private readonly string[] _accessWords = { "SOUTHWEST", "SW" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "SOUTHWEST"; }
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
