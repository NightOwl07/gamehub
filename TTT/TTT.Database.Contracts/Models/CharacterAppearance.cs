namespace TTT.Database.Contracts.Models
{
    public class CharacterAppearance
    {
        public CharacterAppearance(HeadBlend headBlend, HeadOverlay[] headOverlays, FaceFeature[] faceFeatures,
            Component[] components, short eyeColor, short hairColor, short hairColorHighlight)
        {
            this.HeadBlend = headBlend;
            this.HeadOverlays = headOverlays;
            this.FaceFeatures = faceFeatures;
            this.Components = components;
            this.EyeColor = eyeColor;
            this.HairColor = hairColor;
            this.HairColorHighlight = hairColorHighlight;
        }

        public HeadBlend HeadBlend { get; set; }
        public HeadOverlay[] HeadOverlays { get; set; }
        public FaceFeature[] FaceFeatures { get; set; }
        public Component[] Components { get; set; }
        public short EyeColor { get; set; }
        public short HairColor { get; set; }
        public short HairColorHighlight { get; set; }
    }

    public class HeadBlend
    {
        public HeadBlend(short shapeFirstId, short shapeSecondId, short skinFirstId, short skinSecondId, float shapeMix,
            float skinMix)
        {
            this.ShapeFirstId = shapeFirstId;
            this.ShapeSecondId = shapeSecondId;
            this.SkinFirstId = skinFirstId;
            this.SkinSecondId = skinSecondId;
            this.ShapeMix = shapeMix;
            this.SkinMix = skinMix;
        }

        public short ShapeFirstId { get; set; }
        public short ShapeSecondId { get; set; }
        public short ShapeThirdId { get; } = 0;
        public short SkinFirstId { get; set; }
        public short SkinSecondId { get; set; }
        public short SkinThirdId { get; } = 0;
        public float ShapeMix { get; set; }
        public float SkinMix { get; set; }
        public float ThirdMix { get; } = 0;
        public bool IsParent { get; } = false;
    }

    public class HeadOverlay
    {
        public short Id;
        public float Opacity;
        public short Value;

        public HeadOverlay(short id, short value, float opacity = 0, short? color = null)
        {
            this.Id = id;
            this.Value = value;
            this.Opacity = opacity;
            this.Color = color;
        }

        public short? Color { get; set; }
    }

    public class FaceFeature
    {
        public short Id;
        public float Value;

        public FaceFeature(short id, float value)
        {
            this.Id = id;
            this.Value = value;
        }
    }

    public class Component
    {
        public short DrawableId;
        public short Id;
        public short TextureId;

        public Component(short id, short drawableId, short textureId)
        {
            this.Id = id;
            this.DrawableId = drawableId;
            this.TextureId = textureId;
        }
    }
}