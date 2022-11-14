//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfDesingApp.databases
{
    using System;
    using System.Collections.Generic;
    
    public partial class Сотрудник
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Сотрудник()
        {
            this.ДеятельностьСотрудник = new HashSet<ДеятельностьСотрудник>();
            this.УчётнаяЗапись = new HashSet<УчётнаяЗапись>();
        }
    
        public int Код { get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public string Отчество { get; set; }
        public bool Пол { get; set; }
        public System.DateTime ДатаРождения { get; set; }
        public string АдресПроживание { get; set; }
        public string Телефон { get; set; }
        public string ИНН { get; set; }
        public string ОМС { get; set; }
        public string СНИЛС { get; set; }
        public bool СемейноеПоложение { get; set; }
        public System.DateTime ДатаТрудоустройства { get; set; }
        public int КодДолжности { get; set; }
        public string Почта { get; set; }
        public bool ВоеннаяСлужба { get; set; }
        public Nullable<System.DateTime> ДатаУвольнения { get; set; }
        public byte[] Фото { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ДеятельностьСотрудник> ДеятельностьСотрудник { get; set; }
        public virtual Должность Должность { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<УчётнаяЗапись> УчётнаяЗапись { get; set; }
    }
}
