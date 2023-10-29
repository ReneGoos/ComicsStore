using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ComicsStore.Data.Common;
using System.ComponentModel.DataAnnotations;
using ComicsLibrary.Helpers;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class StoryArtistEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _artistId;
        private int? _storyId;
        private ICollection<string> _artistType;
        private ObservableCollection<EnumCheckedType> _roles;
        private ArtistOnlyEditModel _artist;

        private void StoryArtistEditModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ArtistType":
                    if ((_artistType?.Count ?? 0) > 0)
                    {
                        var artistType = _artistType.ToList();

                        foreach (var role in Roles)
                        {
                            var checkStatus = false;
                            foreach (var type in artistType)
                            {
                                if (type.Equals(role.Name))
                                {
                                    checkStatus = true;
                                }
                            }
                            if (role.Checked != checkStatus)
                            {
                                role.Checked = checkStatus;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private ObservableCollection<EnumCheckedType> FillRoles()
        {
            var roles = new ObservableCollection<EnumCheckedType>();

            foreach (var value in Enum.GetValues(typeof(ArtistType))
                                    .Cast<ArtistType>()
                                    .Select(v => v.ToString()))
            {
                var roleType = new EnumCheckedType
                {
                    Checked = false,
                    Name = value
                };
                roleType.PropertyChanged += RoleType_PropertyChanged;
                roles.Add(roleType);
            }

            return roles;
        }

        private void RoleType_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ArtistType = SelectedValues();
        }


        private List<string> SelectedValues()
        {
            var selectedValues = new List<string>();
            foreach (var role in _roles)
            {
                if (role.Checked)
                {
                    selectedValues.Add(role.Name);
                }
            }

            return selectedValues;
        }

        public StoryArtistEditModel() : base()
        {
            PropertyChanged += StoryArtistEditModel_PropertyChanged;
        }

        public int? ArtistId { get => _artistId; set => SetIfValue(ref _artistId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        [CannotBeEmpty(ErrorMessage = "ArtistType cannot be empty")]
        public ICollection<string> ArtistType { get => _artistType; set => Set(ref _artistType, value); }

        public ObservableCollection<EnumCheckedType> Roles { get { if (_roles is null) { _roles = FillRoles(); } return _roles; } set => Set(ref _roles, value); }
        public ArtistOnlyEditModel Artist { get => _artist; set => SetIfValue(ref _artist, value); }

        public int? MainId { get => StoryId; set => StoryId = value; }
        public int? LinkedId { get => ArtistId; set => ArtistId = value; }
        public TableEditModel ChildItem { get => Artist; set => Artist = value as ArtistOnlyEditModel; }
    }
}