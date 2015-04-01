using System;
using System.Collections.Generic;

namespace SharpMud.Commands
{
    public interface ICommand
    {
        String[] AccessWords
        {
            get;
        }

        Permission[] PermissionsRequired
        {
            get;
        }

        void Execute(Connection c, List<string> line);

		string Help
		{
			get;
		}

    }
}
