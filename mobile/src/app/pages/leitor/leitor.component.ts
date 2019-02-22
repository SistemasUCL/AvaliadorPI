import { Component, OnInit, VERSION, ViewChild } from '@angular/core';
import { ZXingScannerComponent } from '@zxing/ngx-scanner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-leitor',
  templateUrl: './leitor.component.html',
  styleUrls: ['./leitor.component.scss']
})
export class LeitorComponent implements OnInit {
  ngVersion = VERSION.full;

  @ViewChild('scanner') scanner: ZXingScannerComponent;

  hasCameras = false;
  hasPermission: boolean;
  qrResultString: string;

  availableDevices: MediaDeviceInfo[];
  selectedDevice: MediaDeviceInfo;

  constructor(private route: Router) {}

  ngOnInit(): void {
    this.scanner.camerasFound.subscribe((devices: MediaDeviceInfo[]) => {
      this.hasCameras = true;

      this.availableDevices = devices;
      // selects the devices's back camera by default
      for (const device of devices) {
        if (/back|rear|environment/gi.test(device.label)) {
          this.scanner.changeDevice(device);
          this.selectedDevice = device;
          break;
        }
      }
    });

    this.scanner.camerasNotFound.subscribe((devices: MediaDeviceInfo[]) => {
      console.error('Nenhuma câmera foi encontrada no dispositivo.');
    });

    this.scanner.permissionResponse.subscribe((answer: boolean) => {
      this.hasPermission = answer;
    });
  }

  handleQrCodeResult(resultString: string) {
    // validar id do grupo e permissão do usuário
    this.qrResultString = resultString;
    this.route.navigateByUrl(`avaliacao/${resultString}`);
  }

  onDeviceSelectChange(selectedValue: string) {
    this.selectedDevice = this.scanner.getDeviceById(selectedValue);
  }
}
