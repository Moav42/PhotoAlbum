import {Pipe, PipeTransform} from '@angular/core';
import { Post } from '../Models/Post';

@Pipe({
    name: 'postsFilter'
})

export class PostFilterPipe implements PipeTransform{
    transform(posts: Post[], search: string = '') : Post[] {
        if(!search.trim()){
            return posts
        }
        return posts.filter( posts => {
            return posts.title.toLowerCase().indexOf(search.toLowerCase()) !== -1
        })
    }

}