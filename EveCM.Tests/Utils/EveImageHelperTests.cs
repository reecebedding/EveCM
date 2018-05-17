using EveCM.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EveCM.Tests
{
    [TestClass]
    public class EveImageHelperTests
    {
        [TestMethod]
        public void GetCharacterAvatar_Should_Return_CorrectUri_DefaultSize()
        {
            string characterId = "12345";
            string expectedUrl = $"https://image.eveonline.com/Character/{characterId}_256.jpg";

            Uri actualUri = EveImageHelper.GetCharacterAvatar(characterId);

            Assert.AreEqual(expectedUrl, actualUri.ToString());
        }

        [TestMethod]
        public void GetCharacterAvatar_Should_Return_CorrectUri_SpecificSize()
        {
            string characterId = "12345";
            EveImageHelper.CharacterAvatarSize size = EveImageHelper.CharacterAvatarSize.Five_Hundred_Twelve;
            string expectedUrl = $"https://image.eveonline.com/Character/{characterId}_512.jpg";

            Uri actualUri = EveImageHelper.GetCharacterAvatar(characterId, size);

            Assert.AreEqual(expectedUrl, actualUri.ToString());
        }
    }
}
