Vue.component('paginate', VuejsPaginate)
Vue.use(window.vuelidate.default)
var { required, helpers } = window.validators
const positiveNumber = helpers.regex('alpha', /^[1-9]\d*$/)

var Naselja = new Vue({
    el: "#naselja",
    data: {
        showTable: false,
        filters: {
            search: "",
            pageNumber: 1,
            pageSize: 10,
            pages: 0,
        },
        numberOfResults: 0,

        initialNaselje: {
            id: 0,
            naziv: "",
            postanskiBroj: "",
            drzavaId: 0,
            drzava: {},
        },
        naselja: [],
        naseljeForEdit: {},
        naseljeForCreate: {},
        naseljaCount: 0,

        drzave: [],
        isLoading: false,
    },
    components: {
        Multiselect: window.VueMultiselect.default,
    },
    validations: {
        naseljeForEdit: {
            naziv: { required },
            postanskiBroj: { required },
            drzavaId: { positiveNumber }
        },
        naseljeForCreate: {
            naziv: { required },
            postanskiBroj: { required },
            drzavaId: { positiveNumber }
        }
    },
    methods: {
        getDrzave() {
            this.isLoading = true;
            axios
                .get('/api/DrzaveApi', {
                    params: {
                        search: this.filters.search.toString()
                    }
                })
                .then(response => {
                    this.drzave = response.data.drzave;
                    this.isLoading = false;
                });
        },
        getNaselja() {
            axios
                .get('/api/NaseljeApi', {
                    params: {
                        pageSize: this.filters.pageSize,
                        pageNumber: this.filters.pageNumber
                    }
                })
                .then(response => {
                    this.naselja = response.data.naselja;
                    this.naseljaCount = response.data.count;
                    this.filters.pages = Math.ceil(this.naseljaCount / this.filters.pageSize);
                    if (this.filters.pages == 0) {
                        this.filters.pages = 1;
                    }
                    this.showTable = true;
                });
        },
        openCreateModal() {
            this.naseljeForCreate = _.cloneDeep(this.initialNaselje);
            $('#createModal').modal('show');
        },
        closeCreateModal() {
            $('#createModal').modal('hide');
            this.naseljeForCreate = {};
        },
        createNaselje() {
            this.$v.naseljeForCreate.$touch();
            if (this.$v.naseljeForCreate.$anyError) {
                return;
            }
            axios
                .post('/api/NaseljeApi', this.naseljeForCreate)
                .then(response => {
                    if (response.data.code == 200) {
                        toastr.success(response.data.status);
                        this.getNaselja();
                        this.closeCreateModal();
                    }
                })
                .catch(function (error) {
                    if (error.response) {
                        toastr.error(error.response.data.status);
                    }
                });
        },
        openEditModal(naselje) {
            this.naseljeForEdit = _.cloneDeep(naselje);
            $('#editModal').modal('show');
        },
        closeEditModal() {
            $('#editModal').modal('hide');
            this.naseljeForEdit = {};
        },
        updateNaselje() {
            axios
                .put('/api/NaseljeApi', this.naseljeForEdit)
                .then(response => {
                    if (response.data.code == 200) {
                        toastr.success(response.data.status);
                        this.getNaselja();
                        this.closeEditModal();
                    }
                })
                .catch(function (error) {
                    if (error.response) {
                        toastr.error(error.response.data.status);
                    }
                });
        },
        deleteNaselje(naselje) {
            axios
                .delete('/api/NaseljeApi/' + naselje.id)
                .then(response => {
                    if (response.data.code == 200) {
                        toastr.success(response.data.status);
                        this.getNaselja();
                    }
                })
                .catch(function (error) {
                    if (error.response) {
                        toastr.error(error.response.data.status);
                    }
                });
        },
        changePageNumber(pageNum) {
            this.filters.pageNumber = pageNum;
            this.getNaselja();
        },
        drzavaSelected(selected) {
            this.naseljeForCreate.drzavaId = selected.id;
        },
        drzavaSearchChange(searchQuery) {
            this.filters.search = searchQuery;
            this.getDrzave();
        },
        drzavaEditSelected(selected) {
            this.naseljeForEdit.drzavaId = selected.id;
        },
        closeMultiselect() {
            this.filters.search = "";
        }
    },
    created() {
        this.getDrzave();
        this.getNaselja();
    }
});