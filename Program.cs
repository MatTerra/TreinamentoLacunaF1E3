using System;

var usuarios = new List<Usuario>()
{
    new Usuario() { Id = 5, Grupo = "Diretoria", Nome = "Carlos" },
    new Usuario() { Id = 21, Grupo = "Diretoria", Nome = "José" },
    new Usuario() { Id = 3, Grupo = "RH", Nome = "Camila" },
    new Usuario() { Id = 42, Grupo = "RH", Nome = "Joana" },
    new Usuario() { Id = 102, Grupo = "", Nome = "Joaquim" },
    new Usuario() { Id = 7, Grupo = "RH", Nome = "Camila" },
    new Usuario() { Id = 105, Grupo = "Operações", Nome = "Vitor" }
};

usuarios.AddRange(new List<Usuario>() {
    new Usuario() { Id = 6, Grupo = "Desenvolvimento", Nome = "Mateus" },
    new Usuario() { Id = 22, Grupo = "Desenvolvimento", Nome = "João" }
});

// A pesquisa com Where é lazy-loaded
var diretoriaComWhere = usuarios.Where(usuario => usuario.Grupo.Equals("Diretoria"));
Console.WriteLine("Resultado Diretoria com Where");
foreach(var membro in diretoriaComWhere)
{
    Console.WriteLine(membro);
}
// A pesquisa com FindAll copia todos os resultados
var diretoriaComFindAll = usuarios.FindAll(usuario => usuario.Grupo.Equals("Diretoria"));
Console.WriteLine("Resultado Diretoria com FindAll");
foreach (var membro in diretoriaComFindAll)
{
    Console.WriteLine(membro);
}
Console.WriteLine("Verificando se há usuário sem Grupo");
if (usuarios.Where(usuario => usuario.Grupo.Equals("")).Any())
{
    Console.WriteLine("Há usuário sem Grupo");
    var usuarioSemGrupo = usuarios.Find(usuario => usuario.Grupo.Equals(""));
    Console.WriteLine(usuarioSemGrupo);
} else
{
    Console.WriteLine("Não há usuário sem grupo");
}
Console.WriteLine("Verificando se há usuário sem Grupo com FirstOrDefault");
if (usuarios.Where(usuario => usuario.Grupo.Equals("")).FirstOrDefault() != default)
{
    Console.WriteLine("Há usuário sem Grupo");
    var usuarioSemGrupo = usuarios.Find(usuario => usuario.Grupo.Equals(""));
    Console.WriteLine(usuarioSemGrupo);
}
else
{
    Console.WriteLine("Não há usuário sem grupo");
}
Console.WriteLine("Buscando Camila");
Console.WriteLine(usuarios.Find(usuario => usuario.Nome.Equals("Camila")));
Console.WriteLine("Lista Crescente de Ids");
var ids = usuarios.OrderBy(usuario => usuario.Id).Select(usuario => usuario.Id);
foreach (var id in ids)
{
    Console.WriteLine(id);
}
Console.WriteLine("Lista Decrescente de Ids");
ids = usuarios.OrderByDescending(usuario => usuario.Id).Select(usuario => usuario.Id);
foreach (var id in ids)
{
    Console.WriteLine(id);
}
Console.WriteLine("Agrupamentos de Usuários");
var usuariosPorGrupo = usuarios.GroupBy(usuario => usuario.Grupo);
foreach(var grupo in usuariosPorGrupo)
{
    Console.Write(grupo.Key);
    Console.Write(" -");
    foreach(var usuario in grupo)
    {
        Console.Write(" ");
        Console.Write(usuario.Nome);
    }
    Console.WriteLine("");
}

// Equivalente ao FindAll, cria uma cópia
var usuariosComoPessoasConvertAll = usuarios.ConvertAll(usuario => new Pessoa() { Nome = usuario.Nome });
Console.WriteLine("Resultado Pessoa com ConvertAll");
foreach (var membro in usuariosComoPessoasConvertAll)
{
    Console.WriteLine(membro);
}

// A conversão com Select é lazy-loaded, como o Where
var usuariosComoPessoasSelect = usuarios.Select(usuario => new Pessoa() { Nome = usuario.Nome });
Console.WriteLine("Resultado Pessoa com Select");
foreach (var membro in usuariosComoPessoasSelect)
{
    Console.WriteLine(membro);
}

public class Usuario
{
    public string Nome { get; set; }
    public int Id { get; set; }
    public string Grupo { get; set; }

    public override string ToString()
    {
        return $"Usuario{{ ID: {this.Id}, Nome: {this.Nome}, Grupo: {this.Grupo} }}";
    }
}

public class Pessoa
{
    public string Nome { get; set; }
    public override string ToString()
    {
        return $"Pessoa{{ Nome: {this.Nome}}}";
    }
}

