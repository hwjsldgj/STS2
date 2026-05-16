using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MoeNegiMod.Moya.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Powers;

public abstract class MoyaPowers : CustomPowerModel
{
    private bool PreviewDegenerate;

    public override string CustomPackedIconPath 
    {
        get
        {
            var degenerateType = PreviewDegenerate ? "_degenerate" : "";
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant() + degenerateType}.png".PowerImagePath();
            Log.Info(">>>[MoeNegiMod]CardPath=" + path, 2);
            return ResourceLoader.Exists(path) ? path : $"card{degenerateType}.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath 
    {
        get
        {
           var degenerateType = PreviewDegenerate ? "_degenerate" : "";
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant() + degenerateType}.png".BigPowerImagePath();
            Log.Info(">>>[MoeNegiMod]CardPath=" + path, 2);
            return ResourceLoader.Exists(path) ? path : $"card{degenerateType}.png".BigPowerImagePath();
        }
    }





}
