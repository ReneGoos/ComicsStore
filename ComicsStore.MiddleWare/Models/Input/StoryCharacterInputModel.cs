﻿namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryCharacterInputModel : BasicCrossInputModel
    {
        public int CharacterId { get; set; }

        public CharacterInputModel Character { get; set; }
    }
}