using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.Database.Contracts.Models;

namespace TTT.Core.Entities
{
    public class TownPlayer : Player, ITownPlayer
    {
        public TownPlayer(IServer server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
        }

        public Dictionary<BodyPart, int> Injuries { get; }

        [JsonIgnore] public Menu.Menu ActiveMenu { get; set; }

        [JsonIgnore] public Account Account { get; set; }

        public ITownPlayer ToAsync(IAsyncContext asyncContext)
        {
            return new Async(this, asyncContext);
        }

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

        private class Async : AsyncPlayer<ITownPlayer>, ITownPlayer
        {
            public Async(ITownPlayer player, IAsyncContext asyncContext) : base(player, asyncContext)
            {
            }

            [JsonIgnore]
            public Account Account
            {
                get => this.BaseObject.Account;
                set => this.BaseObject.Account = value;
            }

            public ITownPlayer ToAsync(IAsyncContext asyncContext)
            {
                return asyncContext == this.AsyncContext ? this : new Async(this.BaseObject, asyncContext);
            }
        }
    }
}