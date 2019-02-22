import { ToastOptions } from 'ng2-toastr';

export class CustomToastOption extends ToastOptions {
  public showCloseButton = true;
  public toastLife = 5000;
  public dismiss = 'auto';
}
