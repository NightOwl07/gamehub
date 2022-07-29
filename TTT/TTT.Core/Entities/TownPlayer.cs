using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.Database.Contracts.Models;

namespace TTT.Core.Entities
{
    public class TownPlayer : AsyncPlayer, ITownPlayer
    {
        public TownPlayer(ICore server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
        }

        public Dictionary<BodyPart, int> Injuries { get; }

        [JsonIgnore] public Menu.Menu ActiveMenu { get; set; }

        [JsonIgnore] public Account Account { get; set; }

        public void FadeScreenIn(int milliseconds)
        {
            this.Emit("TTT:PlayerHandler:FadeInScreen", milliseconds);
        }

        public void FadeScreenOut(int milliseconds)
        {
            this.Emit("TTT:PlayerHandler:FadeOutScreen", milliseconds);
        }

        public void ToggleFadeScreen(int fadeScreenOutMilliseconds, int fadeScreenInMilliseconds)
        {
            AltAsync.Do(async () =>
            {
                await this.EmitAsync("TTT:PlayerHandler:FadeOutScreen", fadeScreenOutMilliseconds);
                await Task.Delay(fadeScreenOutMilliseconds * 2);
                await this.EmitAsync("TTT:PlayerHandler:FadeInScreen", fadeScreenInMilliseconds);
            });
        }
    }
}