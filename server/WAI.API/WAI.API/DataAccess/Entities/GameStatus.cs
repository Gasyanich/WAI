namespace WAI.API.DataAccess.Entities;

public enum GameStatus
{
    /// <summary>
    /// Игра создана (идёт подключение игроков)
    /// </summary>
    Created,
    
    /// <summary>
    /// Идет подготовка (игроки подключились, загадывают слова) 
    /// </summary>
    Prepare,
    
    /// <summary>
    /// Игра началась (слова загаданы, идет сама игра)
    /// </summary>
    Started,
    
    /// <summary>
    /// Игра закончена (все игроки угадали свои слова)
    /// </summary>
    End,
    
    /// <summary>
    /// Игры отменена (создатель отменил игру на любом этапе до попадания ее в статус End)
    /// </summary>
    Canceled 
}