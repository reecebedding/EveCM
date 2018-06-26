export interface IAuthor {
    userName: string,
    avatarUrl: string
}

export interface IBulletin {
    id: number,
    title: string,
    content: string,
    date: Date,
    authorCharacter: IAuthor
}