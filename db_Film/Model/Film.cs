using db_Film.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_Film.Model
{
    public class Film : NotifyModel
    {
        private int id;
        private string name;
        private string producer;
        private string year;
        private string country;
        private string ageRate;
        private string genre;
        private string score;
        private string comment;
        private string type;
       
        public int Id
        {
            get => id;
            set { id = value; RaiseEvent(nameof(Id)); }
        }
        public string Name
        {
            get => name;
            set { name = value; RaiseEvent(nameof(Name)); }
        }
        public string Producer
        {
            get => producer;
            set { producer = value; RaiseEvent(nameof(Producer)); }
        }
        public string Year
        {
            get => year;
            set { year = value; RaiseEvent(nameof(Year)); }
        }
        public string Country
        {
            get => country;
            set { country = value; RaiseEvent(nameof(Country)); }
        }
        public string AgeRate
        {
            get => ageRate;
            set { ageRate = value; RaiseEvent(nameof(AgeRate)); }
        }
        public string Genre
        {
            get => genre;
            set { genre = value; RaiseEvent(nameof(Genre)); }
        }
        public string Score
        {
            get => score;
            set { score = value; RaiseEvent(nameof(Score)); }
        }
        public string Comment
        {
            get => comment;
            set { comment = value; RaiseEvent(nameof(Comment)); }
        }
        public string Type
        {
            get => type;
            set { type = value; RaiseEvent(nameof(Type)); }
        }
    }
}
