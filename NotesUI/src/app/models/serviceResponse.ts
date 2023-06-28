export class ServiceResponse<T> {
    data!: T;
    success!: boolean;
    message!: string;
}