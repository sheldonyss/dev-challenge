import { AppConsts } from './../app-constant';
import {
  EdgeNodeDto,
  NodeServiceProxy,
  PagedResultOfEdgeNodeDto,
  NodeDto,
  EdgeDto
} from './../service-proxies/service-proxies';
import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { DataSet, Network, Data, Edge, Node } from 'vis';
import { MyVisNode } from '../models/vis-node';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  @ViewChild('container')
  container: ElementRef;
  title = 'app';
  nodeMap = {};
  edgeMap = {};
  nodes: DataSet<MyVisNode>;
  edges: DataSet<Edge>;
  networkData: Data;
  network: Network;
  busy = true;
  colorMap = {
    Address: '#91989E',
    Entity: '#A88158',
    Officer: '#9E6958',
    Intermediary: '#3E4248',
    Other: '#F5F3F2'
  };
  legendName: any[] = [];
  constructor(private service: NodeServiceProxy) {
    for (const key in AppConsts.colorMap) {
      if (key) {
        this.legendName.push({
          name: key,
          color: AppConsts.colorMap[key]
        });
      }
    }
  }

  ngOnInit(): void {
    this.initNodes();
  }

  initNodes(): void {
    this.service.getEntity(1, 15).subscribe(
      (ents: PagedResultOfEdgeNodeDto) => {
        this.nodes = new DataSet<MyVisNode>();
        this.edges = new DataSet<Edge>([]);
        ents.items.forEach(n => {
          this.addNode(n.node);
          this.addEdge(n.edge);
        });
        this.networkData = {
          nodes: this.nodes,
          edges: this.edges
        };
        this.network = new Network(
          this.container.nativeElement,
          this.networkData,
          {}
        );
        this.network.on('selectNode', paras => {
          this.onNodeSelected(paras);
        });
        this.busy = false;
      },
      () => {
        this.busy = false;
      }
    );
  }

  private addNode(node: NodeDto): void {
    if (this.nodeMap[node.id] === undefined) {
      this.nodes.add(new MyVisNode(node));
      this.nodeMap[node.id] = true;
    }
  }

  private addEdge(edge: EdgeDto): void {
    if (edge && this.edgeMap[edge.id] === undefined) {
      this.edges.add(edge);
      this.edgeMap[edge.id] = true;
    }
  }

  onNodeSelected(params: any): void {
    if (params && params.nodes) {
      const nodeId = params.nodes[0];
      this.busy = true;
      this.service.expandNode(nodeId).subscribe((data: EdgeNodeDto[]) => {
        this.busy = false;
        data.forEach(d => {
          this.addEdge(d.edge);
          this.addNode(d.node);
        });
      });
    }
  }
}
