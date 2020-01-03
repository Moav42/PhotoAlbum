export class Comment{
    constructor(
        public id?:  number,
        public postId?:  number,
        public userId?: string,
        public text?: string,
        public addingDate?: Date
    ){}
}