using ComicsLibrary.Helpers;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;
using System.Linq;
using ComicsStore.MiddleWare.Models.Output;

namespace ComicsLibrary.EditModels
{
    public class ArtistStoryEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _artistId;
        private int? _storyId;
        private ICollection<string> _artistType;
        private ObservableCollection<EnumCheckedType> _roles;

        private void ArtistStoryEditModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        public ArtistStoryEditModel() : base()
        {
            PropertyChanged += ArtistStoryEditModel_PropertyChanged;
        }

        public int? ArtistId { get => _artistId; set => SetIfValue(ref _artistId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        [Required]
        public ICollection<string> ArtistType { get => _artistType; set => Set(ref _artistType, value); }

        public ObservableCollection<EnumCheckedType> Roles { get { if (_roles is null) { _roles = FillRoles(); } return _roles; } set => Set(ref _roles, value); }
        public StoryOnlyEditModel Story { get; set; }

        public int? MainId { get => ArtistId; set => ArtistId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => Story; set => Story = value as StoryOnlyEditModel; }
    }
}