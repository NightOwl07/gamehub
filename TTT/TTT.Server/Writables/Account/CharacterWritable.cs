using AltV.Net;
using TTT.Database.Contracts.Models;

namespace TTT.Server.Writables.Account
{
    public class CharacterWritable : IWritable
    {
        private readonly CharacterAppearance _characterAppearance;

        public CharacterWritable(CharacterAppearance characterAppearance)
        {
            this._characterAppearance = characterAppearance;
        }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();
            writer.Name("headBlend");
            writer.BeginObject();
            writer.Name("shapeFirstId");
            writer.Value(this._characterAppearance.HeadBlend.ShapeFirstId);
            writer.Name("shapeSecondId");
            writer.Value(this._characterAppearance.HeadBlend.ShapeSecondId);
            writer.Name("shapeThirdId");
            writer.Value(this._characterAppearance.HeadBlend.ShapeThirdId);
            writer.Name("skinFirstId");
            writer.Value(this._characterAppearance.HeadBlend.SkinFirstId);
            writer.Name("skinSecondId");
            writer.Value(this._characterAppearance.HeadBlend.SkinSecondId);
            writer.Name("skinThirdId");
            writer.Value(this._characterAppearance.HeadBlend.SkinThirdId);
            writer.Name("shapeMix");
            writer.Value(this._characterAppearance.HeadBlend.ShapeMix);
            writer.Name("skinMix");
            writer.Value(this._characterAppearance.HeadBlend.SkinMix);
            writer.Name("thirdMix");
            writer.Value(this._characterAppearance.HeadBlend.ThirdMix);
            writer.Name("isParent");
            writer.Value(this._characterAppearance.HeadBlend.IsParent);
            writer.EndObject();

            writer.Name("headOverlays");
            writer.BeginArray();
            foreach (HeadOverlay headOverlay in this._characterAppearance.HeadOverlays)
            {
                writer.BeginObject();
                writer.Name("id");
                writer.Value(headOverlay.Id);
                writer.Name("value");
                writer.Value(headOverlay.Value);
                writer.Name("opacity");
                writer.Value(headOverlay.Opacity);
                if (headOverlay.Color != null)
                {
                    writer.Name("color");
                    writer.Value((int)headOverlay.Color);
                }

                writer.EndObject();
            }

            writer.EndArray();

            writer.Name("faceFeatures");
            writer.BeginArray();
            foreach (FaceFeature faceFeature in this._characterAppearance.FaceFeatures)
            {
                writer.BeginObject();
                writer.Name("id");
                writer.Value(faceFeature.Id);
                writer.Name("value");
                writer.Value(faceFeature.Value);
                writer.EndObject();
            }

            writer.EndArray();

            writer.Name("components");
            writer.BeginArray();
            foreach (Component component in this._characterAppearance.Components)
            {
                writer.BeginObject();
                writer.Name("id");
                writer.Value(component.Id);
                writer.Name("drawableId");
                writer.Value(component.DrawableId);
                writer.Name("textureId");
                writer.Value(component.TextureId);
                writer.EndObject();
            }

            writer.EndArray();

            writer.Name("eyeColor");
            writer.Value(this._characterAppearance.EyeColor);
            writer.Name("hairColor");
            writer.Value(this._characterAppearance.HairColor);
            writer.Name("hairColorHighlight");
            writer.Value(this._characterAppearance.HairColorHighlight);

            writer.EndObject();
        }
    }
}