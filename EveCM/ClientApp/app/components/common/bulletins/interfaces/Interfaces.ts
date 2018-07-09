export interface IAuthor {
    userName: string,
    avatarUrl: string
}

export interface IBulletin {
    id: number,
    title: string,
    content: string,
    createdDate: Date,
    updatedDate?: Date
    authorCharacter: IAuthor
}