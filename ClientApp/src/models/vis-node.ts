import { AppConsts } from '../app-constant';
import { NodeDto } from '../service-proxies/service-proxies';
import { Node } from 'vis';

export class MyVisNode implements Node {
  id: number;
  label: string;
  [key: string]: any;
  constructor(dto: NodeDto) {
    this.id = dto.id;
    this.label = dto.label;
    this.color = {
      background: AppConsts.colorMap[dto.nodeTypeStr]
    };
    this.scaling = {
      min: 15,
      max: 50,
      label: {
        enabled: true
      }
    };
  }
}
