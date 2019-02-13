using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace ComicsStore.Tests
{
    public class EnumHelperTest
    {
        [Fact]
        public void EnumHelper_NoFlags()
        {
            //
            var storyType = StoryType.gag;

            //Act
            var result = EnumHelper<StoryType>.GetDisplayValue(storyType);
            var result1 = EnumHelper<StoryType>.GetDisplayValues(storyType);
            var result2 = EnumHelper<StoryType>.GetValues(storyType);
            var result3 = EnumHelper<StoryType>.GetNames(storyType);
            var results = EnumHelper<StoryType>.GetName(storyType);

            //Assert
            Assert.Equal("Gag", result);
            Assert.Equal(1, result1.Count);
            Assert.Equal("gag", results);
        }

        [Fact]
        public void EnumHelper_Flags_OneValue()
        {
            //
            var artistType = ArtistType.writer;

            //Act
            var result = EnumHelper<ArtistType>.GetDisplayValue(artistType);
            var result1 = EnumHelper<ArtistType>.GetDisplayValues(artistType);
            var result2 = EnumHelper<ArtistType>.GetValues(artistType);
            var result3 = EnumHelper<ArtistType>.GetNames(artistType);
            var results = EnumHelper<ArtistType>.GetName(artistType);

            //Assert
            Assert.Equal("Writer", result);
        }

        [Fact]
        public void EnumHelper_Flags()
        {
            //
            var artistType = ArtistType.inker | ArtistType.writer;

            //Act
            var result = EnumHelper<ArtistType>.GetDisplayValue(artistType);
            var result1 = EnumHelper<ArtistType>.GetDisplayValues(artistType);
            var result2 = EnumHelper<ArtistType>.GetValues(artistType);
            var result3 = EnumHelper<ArtistType>.GetNames(artistType);
            var results = EnumHelper<ArtistType>.GetName(artistType);

            //Assert
            Assert.Equal("Writer, inker", result);
        }

        [Fact]
        public void EnumHelper_FlagsValue()
        {
            //
            var artistType = ArtistType.inker | ArtistType.writer;
            var input = new List<string>() { "writer", "inker" };

            //Act
            var result = EnumHelper<ArtistType>.ParseFlags(input);

            //Assert
            Assert.Equal(artistType, result);
        }

        [Fact]
        public void EnumHelper_FlagsNames()
        {
            //
            var artistType = ArtistType.inker | ArtistType.writer;

            //Act
            var result = EnumHelper<ArtistType>.GetNames(artistType);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void EnumHelper_Names()
        {
            //
            var bookType = BookType.book;

            //Act
            var result = EnumHelper<BookType>.GetNames(bookType);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void EnumHelper_Name()
        {
            //
            var bookType = BookType.book;

            //Act
            var result = EnumHelper<BookType>.GetName(bookType);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal("book", result);
        }
    }
}
