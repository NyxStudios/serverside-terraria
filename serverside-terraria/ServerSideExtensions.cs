using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rests;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using HttpServer;


namespace serverside_terraria
{
	[ApiVersion(1,14)]
	public class ServerSideExtensions : TerrariaPlugin
	{
		public ServerSideExtensions(Main game) : base(game)
		{
		}

		public override void Initialize()
		{
			TShock.RestApi.Register(new SecureRestCommand("/ssc/get/inventory/{user}", GetInventory, "ssc.get.inventory"));
		}

		private object GetInventory(RestVerbs verbs, IParameterCollection parameters, SecureRest.TokenData tokenData)
		{
			List<TSPlayer> players = TShock.Utils.FindPlayer(verbs["user"]);
			if (players.Count > 1)
			{
				return new RestObject("400") {Response = "Found more than one match."};
			}
			else if (players.Count < 1)
			{
				return new RestObject("400") {Response = "Found no matches."};
			}

			string inventory = players[0].PlayerData.inventory.ToString();
			return new RestObject{{"inventory", inventory}};
		}
	}
}
