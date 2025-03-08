<template>
  <div class="min-h-screen bg-gray-900 py-8">
    <div class="container mx-auto px-4">
      <div class="flex justify-end mb-4"></div>
      <h1 class="text-4xl font-bold text-center mb-8 text-white">TODO App</h1>
      <div class="max-w-2xl mx-auto m-4">
        <TodoForm @task-added="fetchTasks" />
      </div>
      <div class="max-w-2xl mx-auto">
        <TodoList v-show="tasks.length !== 0" :tasks="tasks" @task-deleted="fetchTasks" />
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent, onMounted, ref, inject } from 'vue'
import TodoForm from './components/TodoForm.vue'
import TodoList from './components/TodoList.vue'

export default defineComponent({
  components: {
    TodoForm,
    TodoList,
  },
  setup() {
    const $http = inject('$http')
    const tasks = ref([])

    const fetchTasks = async () => {
      const response = await $http.get('/tasks')
      if (Array.isArray(response.data)) {
        tasks.value = response.data
      } else {
        console.error('API вернул не массив')
        tasks.value = []
      }
    }

    onMounted(() => {
      fetchTasks()
    })

    return {
      tasks,
      fetchTasks,
    }
  },
})
</script>
