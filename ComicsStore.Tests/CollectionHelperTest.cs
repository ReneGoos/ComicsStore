using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace ComicsStore.Tests
{
    public class CollectionHelperTest
    {
        [Fact]
        public void IsEqual_OneCollection_ReturnsTrue()
        {
            //Arrange
            var A = new List<StoryBook>()
            {
                new StoryBook()
                {
                    BookId = 1,
                    StoryId = 16,
                    CreationDate = DateTime.Now,
                    DateUpdate = DateTime.Now
                }
            };

            //Act
            //Assert
            Assert.True(CollectionHelper<StoryBook>.IsEqual(A, A, new StoryBookComparer()));
        }

        [Fact]
        public void IsEqual_EqualCollection_ReturnsTrue()
        {
            var date = DateTime.Now;
            //Arrange
            var A = new List<StoryBook>()
            {
                new StoryBook()
                {
                    BookId = 1,
                    StoryId = 16,
                    CreationDate = date,
                    DateUpdate = date
                }
            };
            var B = new List<StoryBook>()
            {
                new StoryBook()
                {
                    BookId = 1,
                    StoryId = 16,
                    CreationDate = date,
                    DateUpdate = date
                }
            };

            //Act
            //Assert
            Assert.True(CollectionHelper<StoryBook>.IsEqual(A, B, new StoryBookComparer()));
        }
    }
}
