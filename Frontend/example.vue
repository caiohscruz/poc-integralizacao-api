<template>
  <div id="app" class="mb-12">
    <h6 class="text-primary">ENADE</h6>
    
		<h4>Arquitetura e Urbanismo & Design</h4>
    
		<div class="callout">
      <div>
        <div class="cardbody p-4 d-flex">
          <img src="./icons/profile.png" style="height: 40px;" class="mr-3">
					<div class="">
            
            <h6 class="textpurple">Histórico de Integralização</h6>
						<h6>Efetue uma busca pelos filtros apresentados logo a baixo. <br />
							Esta tela inicial apresenta a listagem por marcas e as
							posteriores em uma forma mais macro.</h6>
            </div>
          </div>
        </div>
      </div>
      
		<div class="container selects mt-8">
			<div class="row">
				<tr class="grey lighten-3">
					<th v-for="(_, filtro) in selectOptions" :key="filtro">
						<v-autocomplete flat multiple attach chips dense clearable solo :label="filtro"
							:items="selectOptions[filtro]" v-model="filtrosSelecionados[filtro]" class="p-1"
							item-text="nome" item-value="codigo">
							<template v-slot:selection="{ item, index }">
								<v-chip v-if="index < 1">
									<span>
                    {{ item.nome }}
									</span>
								</v-chip>
								<span v-if="index === 1" class="grey--text caption">
									(+{{ filtrosSelecionados[filtro].length - 1 }} e outros)
								</span>
							</template>
						</v-autocomplete>
					</th>
					<div class="col-4 d-flex">
            <label class="mr-4 mt-2 text-primary" @click="getReport()">Limpar seleção</label>
						<button type="button" class="btn btn-primary" @click="getDataFromApi()">Buscar</button>
					</div>
				</tr>
			</div>
		</div>
		
		<div class="mt-5 mb-3 d-flex ">
      
      <div class="md-8 mr-2">
        <h6>Estudantes de Tecnologo para o ENADE</h6>
        
				<div class="card p-3 mb-2 d-flex flex-row">
          <div class="p-2">
            <div class="mb-2">
              <img src="./icons/verify.png">
							Aptos para o ENADE
						</div>
						<h1 class="text-primary fs-1 fw-bold">4.100 </h1>
						<p>estudantes que <br /> possuem 75% de integralização para o ENADE</p>
						<p class="text-primary">Baixar relatorio</p>
            
					</div>
					<div class="p-2 border-start  border-secondary">
            <div class="mb-2">
              <img src="./icons/next.png">
							Próximo do previsto
						</div>
						<h2 class="text-primary fs-1 fw-bold">2.700</h2>
						<p>estudantes que <br /> possuem 55% de integralização para o ENADE</p>
            
					</div>
					<div class="p-2 border-start  border-secondary">
						<div class="mb-2">
              <img src="./icons/alert.png">
							ENADE em 2022
						</div>
						<h2 class=" text-primary fs-1 fw-bold">50</h2>
						<p>estudantes faltaram no ENADE no dia da prova</p>
						<p class="text-primary">Visualizar estudantes</p>

					</div>
				</div>
			</div>
      
			<div class="md-8 ml-2">
        <h6> Estudantes de Tecnologo para o ENADE</h6>
				<div class="card p-3 mb-2 d-flex flex-row ">
          <div class="p-2">
            <div class="mb-2">
              <img src="./icons/verify.png">
							Aptos para o ENADE
						</div>
						<h1 class="text-primary fs-1 fw-bold">4.100 </h1>
						<p>estudantes que <br /> possuem 75% de integralização para o ENADE</p>
						<p class="text-primary">Baixar relatorio</p>
            
            
					</div>
					<div class="p-2 border-start  border-secondary">
            <div class="mb-2">
              <img src="./icons/next.png">
							Próximo do previsto
						</div>
						<h2 class="text-primary fs-1 fw-bold">2.700</h2>
						<p>estudantes que <br /> possuem 55% de integralização para o ENADE</p>
            
					</div>
					<div class="p-2 border-start  border-secondary">
            <div class="mb-2">
              <img src="./icons/alert.png">
							ENADE em 2022
						</div>
						<h2 class=" text-primary fs-1 fw-bold">50</h2>
						<p>estudantes faltaram no ENADE no dia da prova</p>
						<p class="text-primary">Visualizar estudantes</p>
            
					</div>
				</div>
			</div>
      
		</div>
    
    
		<v-data-table :headers="datatable.headers" :items="datatable.items" :server-items-length="datatable.itemsCount"
    :loading="datatable.loading" :options.sync="datatable.options" :page.sync="datatable.options.page"
    loading-text="Carregando... Por favor, aguarde" hide-default-footer	@page-count="datatable.pageCount = $event"		
    >
			<template v-slot:item.regional="{ item }">
				<v-chip :color="getColor(item.regional)" dark>
					{{ item.regional }}
				</v-chip>
			</template>
      
		</v-data-table>
		<v-pagination v-model="datatable.options.page" :length="datatable.pageCount" :total-visible="7"></v-pagination>
    
	</div>
  
</template>


<script>
/* eslint-disable */
import axios from "axios";
import 'datatables.net-bs4';
import "bootstrap/dist/css/bootstrap.min.css";
import "datatables.net-bs4/css/dataTables.bootstrap4.min.css";
import Vuetify from "vuetify";

export default {
	vuetify: new Vuetify(),
	data: () => ({
		datatable: {
			headers: [
				{ text: 'RA', value: 'ra' },
				{ text: 'Nome', value: 'nome' },
				{ text: 'Regional', value: 'regional' },
				{ text: 'Polo', value: 'polo' }
			],
			items: [],
			itemsCount: 0,
			loading: false,
			pageCount: 0,
			options: {
				page: 0,
				itemsPerPage: 15,
				sortBy: [],
				sortDesc: [],
				groupBy: [],
				groupDesc: [],
				multiSort: false,
				mustSort: false
			},
		},
		filtrosSelecionados: {
			Regionais: [],
			Marcas: [],
			Campi: [],
		},
		selectOptions: {
			Regionais: [],
			Marcas: [],
			Campi: []
		},
	}),
	mounted() {
		axios.get('https://localhost:7161/GetRegionais')
			.then(response => {
				this.selectOptions.Regionais = response.data
			})
		axios.get('https://localhost:7161/GetMarcas')
			.then(response => {
				this.selectOptions.Marcas = response.data
			})
		axios.get('https://localhost:7161/GetCampi')
			.then(response => {
				this.selectOptions.Campi = response.data
			})
	},
	watch: {
		'datatable.options': {
			handler() {
				this.getDataFromApi()
			},
			deep: true,
		},
	},
	methods: {
		getDataFromApi() {
			this.datatable.loading = true
			const { page, itemsPerPage } = this.datatable.options
			axios.post('https://localhost:7161/GetAlunos', {
				"draw": 0,
				"columns": [
					{
						"data": "string",
						"name": "CodRegional",
						"searchable": true,
						"orderable": true,
						"search": {
							"value": this.filtrosSelecionados.Regionais.join(','),
							"regex": true
						}
					},
					{
						"data": "string",
						"name": "CodMarca",
						"searchable": true,
						"orderable": true,
						"search": {
							"value": this.filtrosSelecionados.Marcas.join(','),
							"regex": true
						}
					},
					{
						"data": "string",
						"name": "CodCampus",
						"searchable": true,
						"orderable": true,
						"search": {
							"value": this.filtrosSelecionados.Campi.join(','),
							"regex": true
						}
					}
				],
				"order": [
					{
						"column": 0,
						"dir": 0
					}
				],
				"start": (page-1)*itemsPerPage,
				"length": itemsPerPage,
				"search": {
					"value": "string",
					"regex": true
				},
				"additionalValues": [
					"string"
				]
			})
				.then(response => {
					this.datatable.items = response.data.data
					this.datatable.itemsCount = response.data.recordsFiltered
					this.datatable.loading = false
				})
		},
		getReport() {
			axios.post('https://localhost:7161/ExportTable', {
				"draw": 0,
				"columns": [
					{
						"data": "string",
						"name": "CodRegional",
						"searchable": true,
						"orderable": true,
						"search": {
							"value": this.filtrosSelecionados.Regionais.join(','),
							"regex": true
						}
					},
					{
						"data": "string",
						"name": "CodMarca",
						"searchable": true,
						"orderable": true,
						"search": {
							"value": this.filtrosSelecionados.Marcas.join(','),
							"regex": true
						}
					},
					{
						"data": "string",
						"name": "CodCampus",
						"searchable": true,
						"orderable": true,
						"search": {
							"value": this.filtrosSelecionados.Campi.join(','),
							"regex": true
						}
					}
				],
				"order": [
					{
						"column": 0,
						"dir": 0
					}
				],
				"start": 0,
				"length": 0,
				"search": {
					"value": "string",
					"regex": true
				},
				"additionalValues": [
					"string"
				]
			},)
				.then(response => {
					console.log(response)
					let blob = new Blob([response.data], {
						type:
							"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
					}),
						url = window.URL.createObjectURL(blob);
					window.open(url);
				})
		},
		getColor(regional) {
			if (regional == "Norte") return 'green'
			if (regional == "Nordeste") return 'blue'
			if (regional == "Centro-Oeste") return 'yellow'
			if (regional == "Sudeste") return 'red'
			if (regional == "Sul") return 'purple'
		},
	}
}


</script>

<style>
.cardbody{
    background: #FFFFFF;
    
}
.purple{
    
    background: #762582;
}
.textpurple{
    color: #762582;
}
.selects{
    background: rgba(0, 0, 0, 0.05);
}
.callout {
    margin: 20px 0;
    border: 0px solid #762582;
    border-left-width: 10px;
    border-radius: 5px;
}
</style>