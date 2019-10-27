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
        private Dick producer;
        private string year;
        private Dick country;
        private Dick ageRate;
        private Dick genre;
        private string score;
        private string comment;
        private Dick type;
       
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
        public Dick Producer
        {
            get => producer;
            set { producer = value; RaiseEvent(nameof(Producer)); }
        }
        public string Year
        {
            get => year;
            set { year = value; RaiseEvent(nameof(Year)); }
        }
        public Dick Country
        {
            get => country;
            set { country = value; RaiseEvent(nameof(Country)); }
        }
        public Dick AgeRate
        {
            get => ageRate;
            set { ageRate = value; RaiseEvent(nameof(AgeRate)); }
        }
        public Dick Genre
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
        public Dick Type
        {
            get => type;
            set { type = value; RaiseEvent(nameof(Type)); }
        }
    }
}
