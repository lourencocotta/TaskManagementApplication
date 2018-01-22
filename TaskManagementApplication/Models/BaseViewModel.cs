using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementApplication.Models
{
    public class PesquisaViewModelBase
    {
        public bool RecarregarGrid { get; set; }

        public int PaginaAtual { get; set; }
    }

    public class BaseViewModel : IComparable
    {
        public long ID { get; set; }

        public string Abreviacao { get; set; }

        public string Descricao { get; set; }

        public string Mensagem { get; set; }

        public override string ToString()
        {
            return this.Descricao;
        }

        public int CompareTo(object obj)
        {
            var model = (BaseViewModel)obj;
            return this.Descricao.CompareTo(model.Descricao);
        }
    }

    public static class ConverterViewModel
    {
        public static BaseViewModel ConverterParaViewModel(this Task model)
        {
            return new BaseViewModel
            {
                ID = model.Id,
                Descricao = model.TaskName
            };
        }

        public static BaseViewModel ConverterParaViewModel(this TaskList model)
        {
            return new BaseViewModel
            {
                ID = model.Id,
                Descricao = model.Name
            };
        }
    }
}