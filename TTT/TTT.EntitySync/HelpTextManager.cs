using System;
using System.Collections.Generic;
using System.Numerics;
using AltV.Net.EntitySync;

namespace TTT.EntitySync
{
    /// <summary>
    ///     HelpText class that stores all data
    /// </summary>
    public class HelpText : Entity, IEntity
    {
        public static object HelpTextLockHandle = new();
        private static List<HelpText> helpTextList = new();

        public HelpText(Vector3 position, int dimension, uint range, ulong entityType) : base(entityType, position,
            dimension, range)
        {
        }

        /// <summary>
        ///     Set/get the HelpText text.
        /// </summary>
        public string Text
        {
            get
            {
                if (!this.TryGetData("text", out string text))
                    return null;

                return text;
            }
            set => this.SetData("text", value);
        }

        public static List<HelpText> HelpTextList
        {
            get
            {
                lock (HelpTextLockHandle)
                {
                    return helpTextList;
                }
            }
            set => helpTextList = value;
        }

        /// <summary>
        ///     Destroy this textlabel.
        /// </summary>
        public void Delete()
        {
            HelpTextList.Remove(this);
            AltEntitySync.RemoveEntity(this);
        }

        public void SetText(string text)
        {
            this.Text = text;
        }
    }

    public static class HelpTextStreamer
    {
        /// <summary>
        ///     Create a new HelpText.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <returns>The newly created dynamic textlabel.</returns>
        public static HelpText Create(
            string text, Vector3 position, int dimension = 0, uint streamRange = 5
        )
        {
            HelpText helper = new(position, dimension, streamRange, 3)
            {
                Text = text
            };

            HelpText.HelpTextList.Add(helper);
            AltEntitySync.AddEntity(helper);
            return helper;
        }

        /// <summary>
        ///     Destroy HelpText by it's ID.
        /// </summary>
        /// <param name="dynamicTextLabelId">The ID of the text label.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool DeleteHelpText(ulong dynamicTextLabelId)
        {
            HelpText obj = GetHelpText(dynamicTextLabelId);

            if (obj == null)
                return false;

            HelpText.HelpTextList.Remove(obj);
            AltEntitySync.RemoveEntity(obj);
            return true;
        }

        /// <summary>
        ///     Destroy an HelpText.
        /// </summary>
        /// <param name="dynamicTextLabel">The text label instance to destroy.</param>
        public static void DeleteHelpText(HelpText dynamicTextLabel)
        {
            HelpText.HelpTextList.Remove(dynamicTextLabel);
            AltEntitySync.RemoveEntity(dynamicTextLabel);
        }

        /// <summary>
        ///     Get a HelpText by it's ID.
        /// </summary>
        /// <param name="dynamicTextLabelId">The ID of the textlabel.</param>
        /// <returns>The dynamic textlabel or null if not found.</returns>
        public static HelpText GetHelpText(ulong dynamicTextLabelId)
        {
            if (!AltEntitySync.TryGetEntity(dynamicTextLabelId, 4, out IEntity entity))
            {
                Console.WriteLine(
                    $"[OBJECT-STREAMER] [GetDynamicTextLabel] ERROR: Entity with ID {dynamicTextLabelId} couldn't be found.");
                return null;
            }

            return (HelpText)entity;
        }

        /// <summary>
        ///     Destroy all HelpText.
        /// </summary>
        public static void DeleteAllHelpText()
        {
            foreach (HelpText obj in GetAllHelpText())
            {
                HelpText.HelpTextList.Remove(obj);
                AltEntitySync.RemoveEntity(obj);
            }
        }

        /// <summary>
        ///     Get all HelpText.
        /// </summary>
        /// <returns>A list of dynamic textlabels.</returns>
        public static List<HelpText> GetAllHelpText()
        {
            List<HelpText> textLabels = new();

            foreach (IEntity entity in HelpText.HelpTextList)
            {
                HelpText textLabel = GetHelpText(entity.Id);

                if (textLabel != null)
                    textLabels.Add(textLabel);
            }

            return textLabels;
        }
    }
}