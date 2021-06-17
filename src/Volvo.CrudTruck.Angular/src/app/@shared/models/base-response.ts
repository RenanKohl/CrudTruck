export class BaseResponse<T> {
  data: T;
  message: string;
  validationResults: any;
  error: boolean;
}
